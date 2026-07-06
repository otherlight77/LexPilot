import { Box, Button, Paper, Typography } from "@mui/material";

export default function LexMail() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 2 }}>LexMail</Typography>
      <Paper elevation={0} sx={{ p: 3, border: "1px solid #E5E8F0" }}>
        <Typography sx={{ mb: 2 }}>
          Messagerie cabinet : OVH, IMAP, SMTP, classement IA, liaison avec les dossiers.
        </Typography>
        <Button variant="contained">Tester la connexion OVH</Button>
      </Paper>
    </Box>
  );
}
