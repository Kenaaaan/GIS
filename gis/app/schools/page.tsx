"use client";

import dynamic from "next/dynamic";
import { useEffect, useState } from "react";
import { LatLngExpression } from "leaflet";
import { getSkolaData } from "../../services/apiClient"; // Import your API client
import { Skola } from "../../types/Skola"; // Import the Skola type

// Dynamically import MapContainer and other Leaflet components
const MapContainer = dynamic(
  () => import("react-leaflet").then((mod) => mod.MapContainer),
  { ssr: false }
);
const TileLayer = dynamic(
  () => import("react-leaflet").then((mod) => mod.TileLayer),
  { ssr: false }
);
const Marker = dynamic(
  () => import("react-leaflet").then((mod) => mod.Marker),
  { ssr: false }
);
const Popup = dynamic(() => import("react-leaflet").then((mod) => mod.Popup), {
  ssr: false,
});

const SchoolsPage: React.FC = () => {
  const [schools, setSchools] = useState<Skola[]>([]); // State to store schools
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchSchools = async () => {
      try {
        const data = await getSkolaData(); // Fetch schools from the backend
        setSchools(data);
      } catch (error) {
        console.error("Error fetching schools:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchSchools();
  }, []);

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900 p-8">
      <div className="max-w-[1200px] mx-auto bg-gray-100 dark:bg-gray-800 shadow-lg rounded-md overflow-hidden">
        <div className="p-8 sm:p-4 flex flex-col">
          <h1 className="text-2xl font-bold mb-4 text-gray-800 dark:text-gray-200">
            Schools
          </h1>
          <p className="text-gray-700 dark:text-gray-300 mb-6">
            Below is a map of Sarajevo with school locations.
          </p>
          <div className="h-[500px] w-full">
            {loading ? (
              <p className="text-gray-500 dark:text-gray-400">Loading map...</p>
            ) : (
              <MapContainer
                center={[43.8563, 18.4131] as LatLngExpression} // Center on Sarajevo
                zoom={12}
                className="h-full w-full rounded-md"
              >
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                {schools.map((school) => (
                  <Marker
                    key={school.id}
                    position={[
                      school.geometry.coordinates[1], // Latitude
                      school.geometry.coordinates[0], // Longitude
                    ]}
                  >
                    <Popup>
                      <strong>
                        {school.properties.name ||
                          school.properties["name:hr"] ||
                          school.properties["name:sr"] ||
                          school.properties["name:fr"] ||
                          school.properties.old_name ||
                          "name not available"}
                      </strong>
                      <br />
                      {school.properties["addr:city"]
                        ? `${school.properties["addr:postcode"]}, ${
                            school.properties["addr:housenumber"] || ""
                          }`
                        : "No address available"}
                    </Popup>
                  </Marker>
                ))}
              </MapContainer>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default SchoolsPage;
