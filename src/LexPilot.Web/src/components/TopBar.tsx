import { Box, Button, TextField, Typography } from "@mui/material";

export default function TopBar() {
  return (
    <Box sx={{ height: 78, px: 3, display: "flex", alignItems: "center", justifyContent: "space-between", bgcolor: "white", borderBottom: "1px solid #E5E8F0" }}>
      <Box>
        <Typography variant="h6">Bonjour MaÃ®tre</Typography>
        <Typography variant="body2" color="text.secondary">Votre cabinet augmentÃ© par l'IA</Typography>
      </Box>

      <TextField size="small" placeholder="Recherche globale : client, dossier, document..." sx={{ width: 430 }} />

      <Button variant="contained">Nouvelle action</Button>
    </Box>
  );
}
