import { Box, Typography, List, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import DashboardIcon from "@mui/icons-material/Dashboard";
import PeopleIcon from "@mui/icons-material/People";
import FolderIcon from "@mui/icons-material/Folder";
import DescriptionIcon from "@mui/icons-material/Description";
import MailIcon from "@mui/icons-material/Mail";
import SecurityIcon from "@mui/icons-material/Security";
import { NavLink } from "react-router-dom";

const menu = [
  { label: "Dashboard", path: "/", icon: <DashboardIcon /> },
  { label: "Clients", path: "/clients", icon: <PeopleIcon /> },
  { label: "Dossiers", path: "/dossiers", icon: <FolderIcon /> },
  { label: "Documents", path: "/documents", icon: <DescriptionIcon /> },
  { label: "LexMail", path: "/lexmail", icon: <MailIcon /> },
  { label: "LexVault", path: "/lexvault", icon: <SecurityIcon /> }
];

export default function Sidebar() {
  return (
    <Box sx={{ width: 270, bgcolor: "#16213E", color: "white", p: 2.5 }}>
      <Typography variant="h5" sx={{ fontWeight: 900, mb: 0.5 }}>
        LexPilot
      </Typography>
      <Typography variant="body2" sx={{ color: "#B9C4D8", mb: 3 }}>
        Enterprise V0.2
      </Typography>

      <List>
        {menu.map((item) => (
          <ListItemButton
            key={item.path}
            component={NavLink}
            to={item.path}
            sx={{
              borderRadius: 2,
              mb: 1,
              color: "white",
              "&.active": {
                bgcolor: "rgba(255,255,255,0.14)"
              },
              "&:hover": {
                bgcolor: "rgba(255,255,255,0.10)"
              }
            }}
          >
            <ListItemIcon sx={{ color: "white", minWidth: 38 }}>
              {item.icon}
            </ListItemIcon>
            <ListItemText primary={item.label} />
          </ListItemButton>
        ))}
      </List>
    </Box>
  );
}
