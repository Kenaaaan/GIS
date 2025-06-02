import axios from "axios";

const apiConfig = {
  baseURL: "https://localhost:7226/api",
  endpoints: {
    ucenikLimit: "/Ucenik/limit",
    Skola: "/Skola",
    UcenikLokacija: "/Ucenik/lokacija",
    SkolaRecommendations: "/SkolaAnaliza/recommendations",
    SkolaAnalyze: "/SkolaAnaliza/analyze",
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

export const getSkolaData = async (limit = 10) => {
  try {
    const response = await apiClient.get(apiConfig.endpoints.Skola, {
      params: { limit },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching Skola data:", error);
    throw error;
  }
};

export const GetUcenikDataByLocation = async (lokacija: string) => {
  try {
    const response = await apiClient.get(apiConfig.endpoints.UcenikLokacija, {
      params: { lokacija },
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching Ucenik data by location:", error);
    throw error;
  }
};

export const getSkolaRecommendations = async (maxLocations = 5) => {
  try {
    const response = await apiClient.get(
      apiConfig.endpoints.SkolaRecommendations,
      {
        params: { maxLocations },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching school recommendations:", error);
    throw error;
  }
};

export const analyzeSkolaLocations = async (analysisParams: any) => {
  try {
    const response = await apiClient.post(
      apiConfig.endpoints.SkolaAnalyze,
      analysisParams
    );
    return response.data;
  } catch (error) {
    console.error("Error analyzing school locations:", error);
    throw error;
  }
};

export default apiConfig;
