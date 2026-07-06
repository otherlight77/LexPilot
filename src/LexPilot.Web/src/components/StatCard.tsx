import { Card, CardContent, Typography, Box } from "@mui/material";
import type { ReactNode } from "react";

type Props = {
  title: string;
  value: string | number;
  subtitle?: string;
  icon?: ReactNode;
};

export default function StatCard({ title, value, subtitle, icon }: Props) {
  return (
    <Card elevation={0} sx={{ border: "1px solid #E5E8F0" }}>
      <CardContent>
        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
          <Box>
            <Typography variant="body2" color="text.secondary">{title}</Typography>
            <Typography variant="h4" sx={{ mt: 1 }}>{value}</Typography>
            {subtitle && <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>{subtitle}</Typography>}
          </Box>
          <Box sx={{ color: "primary.main" }}>{icon}</Box>
        </Box>
      </CardContent>
    </Card>
  );
}
