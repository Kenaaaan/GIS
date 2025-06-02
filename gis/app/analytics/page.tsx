"use client";

import dynamic from "next/dynamic";
import { useEffect, useState } from "react";
import { LatLngExpression } from "leaflet";
import {
  getSkolaData,
  getSkolaRecommendations,
  analyzeSkolaLocations,
} from "../../services/apiClient";

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
const Circle = dynamic(
  () => import("react-leaflet").then((mod) => mod.Circle),
  { ssr: false }
);

// Interface for recommendation data
interface SchoolRecommendation {
  id: string;
  position: [number, number];
  name: string;
  reason: string;
  priority: "High" | "Medium" | "Low";
  distanceToNearestSchool: string;
  potentialStudents: number;
}

// Empty array for initial recommendations state
const emptyRecommendations: SchoolRecommendation[] = [];

const AnalyticsPage: React.FC = () => {
  const [schools, setSchools] = useState<any[]>([]);
  const [recommendations, setRecommendations] = useState<
    SchoolRecommendation[]
  >([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [maxLocations, setMaxLocations] = useState<number>(5);
  const [loadingRecs, setLoadingRecs] = useState<boolean>(false);
  const [analysisParams, setAnalysisParams] = useState({
    maxLocations: 5,
    minimumDistanceThreshold: 1.0,
    considerPopulationDensity: true,
    considerExistingSchools: true,
  });

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        // Fetch both schools and recommendations in parallel
        const [schoolsData, recommendationsData] = await Promise.all([
          getSkolaData(),
          getSkolaRecommendations(maxLocations),
        ]);

        setSchools(schoolsData);

        // Map the API recommendations to our component format
        if (recommendationsData && Array.isArray(recommendationsData)) {
          const mappedRecommendations = recommendationsData.map((rec: any) => ({
            id: rec.id || `rec-${Math.random().toString(36).substr(2, 9)}`,
            position: [
              Number(rec.location.latitude),
              Number(rec.location.longitude),
            ] as [number, number],
            name: rec.name || `Recommended Location`,
            reason: rec.reason || "Strategic location for new school",
            priority: rec.priority || "Medium",
            distanceToNearestSchool: rec.distanceToNearestSchool
              ? `${rec.distanceToNearestSchool.toFixed(1)} km`
              : "Unknown",
            potentialStudents: rec.potentialStudents || 0,
          }));
          setRecommendations(mappedRecommendations);
        } else {
          console.warn("Invalid recommendations data format, using empty data");
          setRecommendations(emptyRecommendations);
        }
      } catch (error) {
        console.error("Error fetching data:", error);
        setRecommendations(emptyRecommendations);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [maxLocations]);

  const handleAnalyzeLocations = async () => {
    setLoadingRecs(true);
    try {
      const data = await analyzeSkolaLocations(analysisParams);

      // Map the API response to our component format
      if (data && Array.isArray(data)) {
        const mappedRecommendations = data.map((rec: any) => ({
          id: rec.id || `rec-${Math.random().toString(36).substr(2, 9)}`,
          position: [
            Number(rec.location.latitude),
            Number(rec.location.longitude),
          ] as [number, number],
          name: rec.name || `Custom Recommended Location`,
          reason: rec.reason || "Custom analysis result",
          priority: rec.priority || "Medium",
          distanceToNearestSchool: rec.distanceToNearestSchool
            ? `${rec.distanceToNearestSchool.toFixed(1)} km`
            : "Unknown",
          potentialStudents: rec.potentialStudents || 0,
        }));
        setRecommendations(mappedRecommendations);
      }
    } catch (error) {
      console.error("Error analyzing locations:", error);
      alert("Failed to analyze locations. See console for details.");
    } finally {
      setLoadingRecs(false);
    }
  };

  const getPriorityColor = (priority: string) => {
    switch (priority) {
      case "High":
        return "#ff5252"; // Red
      case "Medium":
        return "#ffca28"; // Amber
      case "Low":
        return "#66bb6a"; // Green
      default:
        return "#1976d2"; // Blue
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900 p-8">
      <div className="max-w-[1200px] mx-auto bg-gray-100 dark:bg-gray-800 shadow-lg rounded-md overflow-hidden">
        <div className="p-8 sm:p-4 flex flex-col">
          <h1 className="text-2xl font-bold mb-4 text-gray-800 dark:text-gray-200">
            School Location Analytics
          </h1>
          <p className="text-gray-700 dark:text-gray-300 mb-6">
            Analysis of Sarajevo's educational coverage with recommendations for
            new school locations based on population density and accessibility.
          </p>

          {/* Analysis controls */}
          <div className="bg-gray-200 dark:bg-gray-700 p-4 rounded-md mb-6">
            <h2 className="text-xl font-bold mb-3 text-gray-800 dark:text-gray-200">
              Analysis Controls
            </h2>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4 mb-4">
              <div>
                <label className="block text-gray-700 dark:text-gray-300 mb-1">
                  Max Locations
                </label>
                <input
                  type="number"
                  value={analysisParams.maxLocations}
                  onChange={(e) =>
                    setAnalysisParams({
                      ...analysisParams,
                      maxLocations: parseInt(e.target.value),
                    })
                  }
                  className="w-full p-2 border border-gray-300 rounded-md dark:bg-gray-800 dark:border-gray-600 dark:text-white"
                  min="1"
                  max="10"
                />
              </div>

              <div>
                <label className="block text-gray-700 dark:text-gray-300 mb-1">
                  Min Distance (km)
                </label>
                <input
                  type="number"
                  value={analysisParams.minimumDistanceThreshold}
                  onChange={(e) =>
                    setAnalysisParams({
                      ...analysisParams,
                      minimumDistanceThreshold: parseFloat(e.target.value),
                    })
                  }
                  className="w-full p-2 border border-gray-300 rounded-md dark:bg-gray-800 dark:border-gray-600 dark:text-white"
                  step="0.1"
                  min="0.1"
                />
              </div>

              <div className="flex items-center">
                <input
                  type="checkbox"
                  id="considerpopulation"
                  checked={analysisParams.considerPopulationDensity}
                  onChange={(e) =>
                    setAnalysisParams({
                      ...analysisParams,
                      considerPopulationDensity: e.target.checked,
                    })
                  }
                  className="mr-2"
                />
                <label
                  htmlFor="considerpopulation"
                  className="text-gray-700 dark:text-gray-300"
                >
                  Consider Population Density
                </label>
              </div>

              <div className="flex items-center">
                <input
                  type="checkbox"
                  id="considerexisting"
                  checked={analysisParams.considerExistingSchools}
                  onChange={(e) =>
                    setAnalysisParams({
                      ...analysisParams,
                      considerExistingSchools: e.target.checked,
                    })
                  }
                  className="mr-2"
                />
                <label
                  htmlFor="considerexisting"
                  className="text-gray-700 dark:text-gray-300"
                >
                  Consider Existing Schools
                </label>
              </div>
            </div>

            <div className="flex gap-4">
              <button
                onClick={() => setMaxLocations(analysisParams.maxLocations)}
                className="px-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600 transition"
                disabled={loading}
              >
                Refresh Recommendations
              </button>

              <button
                onClick={handleAnalyzeLocations}
                className="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition"
                disabled={loadingRecs}
              >
                {loadingRecs ? "Analyzing..." : "Custom Analysis"}
              </button>
            </div>
          </div>

          <div className="h-[500px] w-full mb-6">
            {loading ? (
              <p className="text-gray-500 dark:text-gray-400">Loading map...</p>
            ) : (
              <MapContainer
                center={[43.8563, 18.4131] as LatLngExpression}
                zoom={12}
                className="h-full w-full rounded-md"
              >
                <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />

                {/* Display existing schools */}
                {schools.map((school) => (
                  <Marker
                    key={school.id}
                    position={[
                      school.geometry.coordinates[1],
                      school.geometry.coordinates[0],
                    ]}
                  >
                    <Popup>
                      <strong>Existing School:</strong>
                      <br />
                      {school.properties.name ||
                        school.properties["name:hr"] ||
                        school.properties["name:sr"] ||
                        "Unknown School"}
                    </Popup>
                  </Marker>
                ))}

                {/* Display recommended locations */}
                {recommendations.map((spot) => (
                  <Circle
                    key={spot.id}
                    center={spot.position as LatLngExpression}
                    radius={500}
                    pathOptions={{
                      fillColor: getPriorityColor(spot.priority),
                      fillOpacity: 0.5,
                      color: getPriorityColor(spot.priority),
                      weight: 2,
                    }}
                  >
                    <Popup>
                      <div className="p-2">
                        <h3 className="font-bold text-lg">{spot.name}</h3>
                        <p className="text-sm my-1">
                          <span className="font-bold">Priority:</span>{" "}
                          <span
                            style={{ color: getPriorityColor(spot.priority) }}
                          >
                            {spot.priority}
                          </span>
                        </p>
                        <p className="text-sm my-1">
                          <span className="font-bold">Reason:</span>{" "}
                          {spot.reason}
                        </p>
                        <p className="text-sm my-1">
                          <span className="font-bold">
                            Distance to nearest school:
                          </span>{" "}
                          {spot.distanceToNearestSchool}
                        </p>
                        <p className="text-sm my-1">
                          <span className="font-bold">Potential students:</span>{" "}
                          {spot.potentialStudents}
                        </p>
                      </div>
                    </Popup>
                  </Circle>
                ))}
              </MapContainer>
            )}
          </div>

          <div className="bg-gray-200 dark:bg-gray-700 p-4 rounded-md">
            <h2 className="text-xl font-bold mb-3 text-gray-800 dark:text-gray-200">
              Recommendation Legend
            </h2>
            <div className="flex flex-wrap gap-4">
              <div className="flex items-center">
                <div
                  className="w-4 h-4 rounded-full mr-2"
                  style={{ backgroundColor: getPriorityColor("High") }}
                ></div>
                <span className="text-gray-800 dark:text-gray-200">
                  High Priority
                </span>
              </div>
              <div className="flex items-center">
                <div
                  className="w-4 h-4 rounded-full mr-2"
                  style={{ backgroundColor: getPriorityColor("Medium") }}
                ></div>
                <span className="text-gray-800 dark:text-gray-200">
                  Medium Priority
                </span>
              </div>
              <div className="flex items-center">
                <div
                  className="w-4 h-4 rounded-full mr-2"
                  style={{ backgroundColor: getPriorityColor("Low") }}
                ></div>
                <span className="text-gray-800 dark:text-gray-200">
                  Low Priority
                </span>
              </div>
            </div>
          </div>

          {/* Recommendations table */}
          <div className="mt-6">
            <h2 className="text-xl font-bold mb-3 text-gray-800 dark:text-gray-200">
              Recommended Locations
            </h2>
            <div className="overflow-x-auto">
              <table className="min-w-full bg-white dark:bg-gray-800 rounded-md overflow-hidden shadow">
                <thead>
                  <tr className="bg-gray-200 dark:bg-gray-700">
                    <th className="py-2 px-4 text-left">Name</th>
                    <th className="py-2 px-4 text-left">Priority</th>
                    <th className="py-2 px-4 text-left">Distance</th>
                    <th className="py-2 px-4 text-left">Potential Students</th>
                    <th className="py-2 px-4 text-left">Reason</th>
                  </tr>
                </thead>
                <tbody>
                  {recommendations.map((rec) => (
                    <tr
                      key={rec.id}
                      className="border-t border-gray-200 dark:border-gray-700"
                    >
                      <td className="py-2 px-4">{rec.name}</td>
                      <td className="py-2 px-4">
                        <span
                          className="px-2 py-1 rounded-full text-xs font-bold"
                          style={{
                            backgroundColor: getPriorityColor(rec.priority),
                            color: "white",
                          }}
                        >
                          {rec.priority}
                        </span>
                      </td>
                      <td className="py-2 px-4">
                        {rec.distanceToNearestSchool}
                      </td>
                      <td className="py-2 px-4">{rec.potentialStudents}</td>
                      <td className="py-2 px-4">{rec.reason}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AnalyticsPage;
