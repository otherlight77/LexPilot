import { Box, Button, Paper, Typography } from "@mui/material";

export default function Documents() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 2 }}>Documents</Typography>
      <Paper elevation={0} sx={{ p: 3, border: "1px solid #E5E8F0" }}>
        <Typography sx={{ mb: 2 }}>GED, dÃ©pÃ´t de fichiers, OCR et analyse IA.</Typography>
        <Button variant="contained">Ajouter un document</Button>
      </Paper>
    </Box>
  );
}
