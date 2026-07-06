import { Grid, Typography, Paper, Box } from "@mui/material";
import PeopleIcon from "@mui/icons-material/People";
import FolderIcon from "@mui/icons-material/Folder";
import DescriptionIcon from "@mui/icons-material/Description";
import MailIcon from "@mui/icons-material/Mail";
import StatCard from "../components/StatCard";

export default function Dashboard() {
  return (
    <Box>
      <Typography variant="h4" sx={{ mb: 1 }}>
        Tableau de bord
      </Typography>
      <Typography color="text.secondary" sx={{ mb: 3 }}>
        Vue synthÃ©tique du cabinet, des dossiers, des documents et de l'activitÃ© IA.
      </Typography>

      <Grid container spacing={2}>
        <Grid item xs={12} md={3}>
          <StatCard title="Clients" value="1" subtitle="Base de dÃ©monstration" icon={<PeopleIcon fontSize="large" />} />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Dossiers" value="0" subtitle="Ã€ crÃ©er" icon={<FolderIcon fontSize="large" />} />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Documents" value="0" subtitle="Analyse IA prÃªte" icon={<DescriptionIcon fontSize="large" />} />
        </Grid>
        <Grid item xs={12} md={3}>
          <StatCard title="Mails" value="OVH" subtitle="IMAP / SMTP" icon={<MailIcon fontSize="large" />} />
        </Grid>
      </Grid>

      <Paper elevation={0} sx={{ mt: 3, p: 3, border: "1px solid #E5E8F0" }}>
        <Typography variant="h6">Objectif V0.2</Typography>
        <Typography color="text.secondary" sx={{ mt: 1 }}>
          Interface initiale LexPilot : dashboard, clients, dossiers, documents, LexMail et LexVault.
        </Typography>
      </Paper>
    </Box>
  );
}
