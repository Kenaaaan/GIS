/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./app/**/*.{js,ts,jsx,tsx}"], // Ensure Tailwind scans your files
  theme: {
    extend: {}, // Extend the default theme if needed
  },
  darkMode: "class", // Enable class-based dark mode
  plugins: [],
};
