import { Routes, Route, Navigate } from "react-router-dom";
import MainLayout from "./layouts/MainLayout";
import Dashboard from "./pages/Dashboard";
import Clients from "./pages/Clients";
import Cases from "./pages/Cases";
import Documents from "./pages/Documents";
import LexMail from "./pages/LexMail";
import LexVault from "./pages/LexVault";
import Login from "./pages/Login";

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />

      <Route path="/" element={<MainLayout />}>
        <Route index element={<Dashboard />} />
        <Route path="clients" element={<Clients />} />
        <Route path="dossiers" element={<Cases />} />
        <Route path="documents" element={<Documents />} />
        <Route path="lexmail" element={<LexMail />} />
        <Route path="lexvault" element={<LexVault />} />
      </Route>

      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}
