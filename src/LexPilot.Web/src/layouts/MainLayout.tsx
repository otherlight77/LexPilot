import { Box } from "@mui/material";
import { Outlet } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import TopBar from "../components/TopBar";
import AIAssistant from "../components/AIAssistant";

export default function MainLayout() {
  return (
    <Box sx={{ display: "flex", minHeight: "100vh", bgcolor: "background.default" }}>
      <Sidebar />

      <Box sx={{ flex: 1, display: "flex", flexDirection: "column" }}>
        <TopBar />

        <Box sx={{ display: "flex", flex: 1 }}>
          <Box sx={{ flex: 1, p: 3 }}>
            <Outlet />
          </Box>

          <AIAssistant />
        </Box>
      </Box>
    </Box>
  );
}
