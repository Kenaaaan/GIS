"use client";
import React, { useState, useEffect } from "react";
import {
  FaHome,
  FaUserGraduate,
  FaCog,
  FaChartBar,
  FaBars,
  FaTimes,
  FaMoon,
  FaSun,
} from "react-icons/fa";

const Sidebar: React.FC = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [isDarkMode, setIsDarkMode] = useState(false);

  const toggleSidebar = () => {
    setIsOpen(!isOpen);
  };

  const toggleDarkMode = () => {
    const newMode = !isDarkMode;
    setIsDarkMode(newMode);

    if (newMode) {
      document.documentElement.classList.add("dark");
      localStorage.setItem("theme", "dark");
    } else {
      document.documentElement.classList.remove("dark");
      localStorage.setItem("theme", "light");
    }
  };

  // Load the saved theme on component mount
  useEffect(() => {
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme === "dark") {
      setIsDarkMode(true);
      document.documentElement.classList.add("dark");
    }
  }, []);

  return (
    <div className="relative">
      {/* Hamburger Menu for Small Screens */}
      <button
        className="absolute top-4 left-4 z-20 md:hidden text-gray-800 dark:text-gray-200"
        onClick={toggleSidebar}
      >
        {isOpen ? (
          <FaTimes className="text-2xl" />
        ) : (
          <FaBars className="text-2xl" />
        )}
      </button>

      {/* Sidebar */}
      <div
        className={`h-screen ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        } transition-transform md:translate-x-0 fixed md:relative z-10 bg-gradient-to-b from-gray-100 to-gray-50 dark:from-gray-800 dark:to-gray-900 text-gray-800 dark:text-gray-200 shadow-lg flex flex-col ${
          isOpen ? "w-40 sm:w-36" : "w-64"
        } md:w-64`}
      >
        <div className="p-3 sm:p-4 md:p-6 text-center text-lg sm:text-xl md:text-2xl font-bold border-b border-gray-200 dark:border-gray-700">
          Spatial Data Analysis
        </div>
        <nav className="flex-1 p-2 sm:p-3 md:p-4">
          <ul className="space-y-2 sm:space-y-3 md:space-y-4">
            <li>
              <a
                href="/"
                className="flex items-center space-x-2 sm:space-x-3 p-2 sm:p-3 rounded-md hover:bg-gray-200 dark:hover:bg-gray-700 transition"
              >
                <FaHome className="text-base sm:text-lg text-blue-500" />
                <span className="text-sm sm:text-base font-medium">Home</span>
              </a>
            </li>
            <li>
              <a
                href="/students"
                className="flex items-center space-x-2 sm:space-x-3 p-2 sm:p-3 rounded-md hover:bg-gray-200 dark:hover:bg-gray-700 transition"
              >
                <FaUserGraduate className="text-base sm:text-lg text-green-500" />
                <span className="text-sm sm:text-base font-medium">
                  Students
                </span>
              </a>
            </li>
            <li>
              <a
                href="#"
                className="flex items-center space-x-2 sm:space-x-3 p-2 sm:p-3 rounded-md hover:bg-gray-200 dark:hover:bg-gray-700 transition"
              >
                <FaChartBar className="text-base sm:text-lg text-purple-500" />
                <span className="text-sm sm:text-base font-medium">
                  Reports
                </span>
              </a>
            </li>
            <li>
              <a
                href="#"
                className="flex items-center space-x-2 sm:space-x-3 p-2 sm:p-3 rounded-md hover:bg-gray-200 dark:hover:bg-gray-700 transition"
              >
                <FaCog className="text-base sm:text-lg text-gray-500" />
                <span className="text-sm sm:text-base font-medium">
                  Settings
                </span>
              </a>
            </li>
          </ul>
        </nav>
        <div className="p-3 sm:p-4 md:p-6 border-t border-gray-200 dark:border-gray-700">
          <button
            onClick={toggleDarkMode}
            className="flex items-center justify-center w-full p-2 sm:p-3 rounded-md bg-gray-200 dark:bg-gray-700 hover:bg-gray-300 dark:hover:bg-gray-600 transition"
          >
            {isDarkMode ? (
              <>
                <FaSun className="text-yellow-500 mr-2" />
                <span className="text-sm sm:text-base">Light Mode</span>
              </>
            ) : (
              <>
                <FaMoon className="text-blue-500 mr-2" />
                <span className="text-sm sm:text-base">Dark Mode</span>
              </>
            )}
          </button>
          <p className="text-xs sm:text-sm md:text-base text-center text-gray-500 dark:text-gray-400 mt-3">
            Kenan Abadžić
          </p>
        </div>
      </div>
    </div>
  );
};

export default Sidebar;
