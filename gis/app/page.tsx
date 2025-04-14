"use client";

import React from "react";

const App: React.FC = () => {
  return (
    <div className="p-8 sm:p-4 bg-gray-50 dark:bg-gray-900 overflow-y-auto">
      <div className="flex flex-col items-center">
        <img src="/unsa-logo.jpg" alt="UNSA Logo" className="w-32 h-32 mb-6" />
        <h1 className="text-4xl md:text-3xl sm:text-2xl font-bold mb-4 text-gray-800 dark:text-gray-200 text-center">
          Spatial Data Analysis in NoSQL Databases
        </h1>
        <p className="text-lg sm:text-md text-gray-800 dark:text-gray-300 text-center mb-3">
          <strong>Kenan Abadžić</strong>
        </p>
        <p className="text-md sm:text-sm text-gray-600 dark:text-gray-400 text-center mb-5">
          Mentor: <strong>Almir Karabegović</strong>
        </p>
        <p className="text-md sm:text-sm text-gray-600 dark:text-gray-400 text-center">
          The goal of this project is to explore and analyze spatial data using
          NoSQL databases (MongoDB), showcasing the potential of modern database
          systems in handling complex geospatial data.
        </p>
      </div>
    </div>
  );
};

export default App;
