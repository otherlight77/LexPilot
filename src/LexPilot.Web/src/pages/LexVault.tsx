import { Box, Button, Paper, Typography } from "@mui/material";

export default function LexVault() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 2 }}>LexVault</Typography>
      <Paper elevation={0} sx={{ p: 3, border: "1px solid #E5E8F0" }}>
        <Typography sx={{ mb: 2 }}>
          Coffre-fort numÃ©rique : documents sensibles, journal d'accÃ¨s, partage sÃ©curisÃ©.
        </Typography>
        <Button variant="contained">Ajouter au coffre-fort</Button>
      </Paper>
    </Box>
  );
}
