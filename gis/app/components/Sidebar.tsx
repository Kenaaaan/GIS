"use client";
import React, { useState, useEffect } from "react";
import {
  FaHome,
  FaUserGraduate,
  FaSchool,
  FaCog,
  FaMoon,
  FaSun,
} from "react-icons/fa";

const Sidebar: React.FC = () => {
  const [isDarkMode, setIsDarkMode] = useState(false);

  const toggleDarkMode = () => {
    const htmlElement = document.documentElement;
    if (htmlElement.classList.contains("dark")) {
      htmlElement.classList.remove("dark");
      localStorage.setItem("theme", "light");
      setIsDarkMode(false);
    } else {
      htmlElement.classList.add("dark");
      localStorage.setItem("theme", "dark");
      setIsDarkMode(true);
    }
  };

  useEffect(() => {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark") {
      document.documentElement.classList.add("dark");
      setIsDarkMode(true);
    }
  }, []);

  return (
    <div className="h-screen w-64 bg-gradient-to-b from-gray-200 to-gray-100 dark:from-gray-800 dark:to-gray-900 text-gray-800 dark:text-gray-200 shadow-lg flex flex-col">
      <div className="p-4 text-center text-lg font-bold border-b border-gray-300 dark:border-gray-700">
        Spatial Data Analysis
      </div>
      <nav className="flex-1 p-4 overflow-y-auto">
        <ul className="space-y-4">
          {/* Home */}
          <li>
            <a
              href="/"
              className="flex items-center space-x-3 p-3 rounded-md hover:bg-gray-300 dark:hover:bg-gray-700 transition"
            >
              <FaHome className="text-lg text-blue-500" />
              <span className="font-medium">Home</span>
            </a>
          </li>

          {/* Students */}
          <li>
            <a
              href="/students"
              className="flex items-center space-x-3 p-3 rounded-md hover:bg-gray-300 dark:hover:bg-gray-700 transition"
            >
              <FaUserGraduate className="text-lg text-green-500" />
              <span className="font-medium">Students</span>
            </a>
          </li>

          {/* Schools */}
          <li>
            <a
              href="/schools"
              className="flex items-center space-x-3 p-3 rounded-md hover:bg-gray-300 dark:hover:bg-gray-700 transition"
            >
              <FaSchool className="text-lg text-purple-500" />
              <span className="font-medium">Schools</span>
            </a>
          </li>

          {/* Settings */}
          <li>
            <a
              href="/settings"
              className="flex items-center space-x-3 p-3 rounded-md hover:bg-gray-300 dark:hover:bg-gray-700 transition"
            >
              <FaCog className="text-lg text-gray-500" />
              <span className="font-medium">Settings</span>
            </a>
          </li>
        </ul>
      </nav>
      <div className="p-4 border-t border-gray-300 dark:border-gray-700">
        <button
          onClick={toggleDarkMode}
          className="flex items-center justify-center w-full p-3 rounded-md bg-gray-300 dark:bg-gray-700 hover:bg-gray-400 dark:hover:bg-gray-600 transition"
        >
          {isDarkMode ? (
            <>
              <FaSun className="text-yellow-500 mr-2" />
              <span>Light Mode</span>
            </>
          ) : (
            <>
              <FaMoon className="text-blue-500 mr-2" />
              <span>Dark Mode</span>
            </>
          )}
        </button>
      </div>
    </div>
  );
};

export default Sidebar;
