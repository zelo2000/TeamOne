import { NodeType } from "./NodeType";

export interface ToDoNodeBaseModel {
    Name?: string;
    Description?: string;
    Type: NodeType;
    Date?: string;
}