import instanceApi from '../utils/instanceApi';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { NodeStatus } from '../models/NodeStatus';
import { ToDoNodeModel } from '../models/ToDoNodeModel';

const getByTripId = async (tripId: string): Promise<ToDoNodeModel[]> => {
    return await instanceApi.get(`/todonode/${tripId}`);
}

const create = async (tripId: string, toDoNode: ToDoNodeBaseModel): Promise<void> => {
    return await instanceApi.post(`/todonode/${tripId}`, toDoNode);
};

const save = async (nodeId: string, toDoNode: ToDoNodeBaseModel): Promise<void> => {
    return await instanceApi.put(`/todonode/${nodeId}`, toDoNode);
};

const remove = async (nodeId: string): Promise<void> => {
    return await instanceApi.delete(`/todonode/${nodeId}`);
}

const updateStatus = async (nodeId: string, status: NodeStatus): Promise<void> => {
    return await instanceApi.patch(`/todonode/${nodeId}/${status}`);
};

const ToDoService = {
    getByTripId,
    create,
    save,
    remove,
    updateStatus
};

export default ToDoService;