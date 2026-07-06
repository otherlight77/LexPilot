import { Box, Button, Divider, Typography } from "@mui/material";

export default function AIAssistant() {
  return (
    <Box sx={{ width: 330, bgcolor: "white", borderLeft: "1px solid #E5E8F0", p: 2.5 }}>
      <Typography variant="h6">LexPilot Cortex</Typography>
      <Typography variant="body2" color="text.secondary" sx={{ mt: 0.5 }}>
        Assistant IA permanent
      </Typography>

      <Divider sx={{ my: 2 }} />

      <Typography variant="body2">
        PrioritÃ©s dÃ©tectÃ©es :
      </Typography>

      <Box component="ul" sx={{ pl: 2, color: "text.secondary" }}>
        <li>3 nouveaux mails Ã  classer</li>
        <li>2 dossiers avec Ã©chÃ©ance proche</li>
        <li>1 document Ã  analyser</li>
      </Box>

      <Button fullWidth variant="contained" sx={{ mt: 2 }}>
        Analyser un document
      </Button>

      <Button fullWidth variant="outlined" sx={{ mt: 1 }}>
        PrÃ©parer un courrier
      </Button>

      <Button fullWidth variant="outlined" sx={{ mt: 1 }}>
        Classer les mails
      </Button>
    </Box>
  );
}
