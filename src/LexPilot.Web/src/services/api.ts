import axios from "axios";

export const api = axios.create({
  baseURL: "http://localhost:5000"
});

export async function getDashboard() {
  const response = await api.get("/api/dashboard");
  return response.data;
}

export async function getClients() {
  const response = await api.get("/api/clients");
  return response.data;
}

export async function getCases() {
  const response = await api.get("/api/cases");
  return response.data;
}

export async function getDocuments() {
  const response = await api.get("/api/documents");
  return response.data;
}

export async function getInbox() {
  const response = await api.get("/api/mail/inbox?max=10");
  return response.data;
}
