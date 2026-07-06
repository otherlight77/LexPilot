import { Box, Button, Paper, Typography } from "@mui/material";

export default function Cases() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 2 }}>Dossiers</Typography>
      <Paper elevation={0} sx={{ p: 3, border: "1px solid #E5E8F0" }}>
        <Typography sx={{ mb: 2 }}>Gestion des dossiers, statuts, notes et Ã©chÃ©ances.</Typography>
        <Button variant="contained">CrÃ©er un dossier</Button>
      </Paper>
    </Box>
  );
}
