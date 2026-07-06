import { Box, Button, Paper, Typography } from "@mui/material";

export default function Clients() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 2 }}>Clients</Typography>
      <Paper elevation={0} sx={{ p: 3, border: "1px solid #E5E8F0" }}>
        <Typography sx={{ mb: 2 }}>Gestion des clients du cabinet.</Typography>
        <Button variant="contained">Ajouter un client</Button>
      </Paper>
    </Box>
  );
}
