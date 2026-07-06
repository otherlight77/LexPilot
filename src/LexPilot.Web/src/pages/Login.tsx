import { Box, Button, Paper, TextField, Typography } from "@mui/material";

export default function Login() {
  return (
    <Box sx={{ minHeight: "100vh", display: "flex", alignItems: "center", justifyContent: "center", bgcolor: "#F4F6FA" }}>
      <Paper sx={{ width: 420, p: 4 }}>
        <Typography variant="h4" sx={{ mb: 1 }}>LexPilot</Typography>
        <Typography color="text.secondary" sx={{ mb: 3 }}>Connexion cabinet</Typography>
        <TextField fullWidth label="Email" sx={{ mb: 2 }} />
        <TextField fullWidth label="Mot de passe" type="password" sx={{ mb: 2 }} />
        <Button fullWidth variant="contained">Se connecter</Button>
      </Paper>
    </Box>
  );
}
