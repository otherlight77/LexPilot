import { createTheme } from "@mui/material/styles";

export const lexPilotTheme = createTheme({
  palette: {
    mode: "light",
    primary: {
      main: "#16213E"
    },
    secondary: {
      main: "#2F80ED"
    },
    background: {
      default: "#F4F6FA",
      paper: "#FFFFFF"
    }
  },
  typography: {
    fontFamily: "Inter, Segoe UI, Arial, sans-serif",
    h4: {
      fontWeight: 800
    },
    h6: {
      fontWeight: 700
    }
  },
  shape: {
    borderRadius: 14
  }
});
