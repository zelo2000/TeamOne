import { ToDoNodeBaseModel } from "./ToDoNodeBaseModel";
import { NodeStatus } from "./NodeStatus";

export interface ToDoNodeModel extends ToDoNodeBaseModel {
    Id: string;
    Status: NodeStatus;
}