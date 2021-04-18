import { Guid } from "guid-typescript";

export interface Contact {
  id?: Guid | null;
  name: string;
  phone: string;
  email: string;
  company: string;
}
