import axios from "axios";

const apiUrl = "https://localhost:7226/api/Ucenik";

// Function to get Ucenici data
export const getUceniciData = async (limit = 10) => {
  try {
    const response = await axios.get(apiUrl, {
      params: { limit },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching Ucenici data:", error);
    throw error;
  }
};
