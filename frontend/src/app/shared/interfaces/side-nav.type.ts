import { TipoUsuario } from "web-api-client";

export interface SideNavInterface {
    path: string;
    title: string;
    iconType: "" | "nzIcon" | "fontawesome" | "lucide";
    iconTheme?: "" | "fab" | "far" | "fas" | "fill" | "outline" | "twotone";
    icon: string,
    submenu: SideNavInterface[];
    permissions?: TipoUsuario[];
}
