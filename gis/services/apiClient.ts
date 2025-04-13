import axios from "axios";

const apiConfig = {
  baseURL: "https://localhost:7226/api",
  endpoints: {
    ucenikLimit: "/Ucenik/limit",
    ucenikSvi: "/Ucenik/svi",
    Skola: "/Skola",
  },
};

const apiClient = axios.create({
  baseURL: apiConfig.baseURL,
});

export const getUceniciDataLimit = async (limit = 10) => {
  try {
    const response = await apiClient.get(apiConfig.endpoints.ucenikLimit, {
      params: { limit },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching Ucenici data:", error);
    throw error;
  }
};

export const getUceniciDataSvi = async () => {
  try {
    const response = await apiClient.get(apiConfig.endpoints.ucenikSvi);
    return response.data;
  } catch (error) {
    console.error("Error fetching Ucenici data:", error);
    throw error;
  }
};

export default apiConfig;
