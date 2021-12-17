import { NodeType } from "./NodeType";

export interface ToDoNodeBaseModel {
    name?: string;
    description?: string;
    type: NodeType;
    date?: string;
}