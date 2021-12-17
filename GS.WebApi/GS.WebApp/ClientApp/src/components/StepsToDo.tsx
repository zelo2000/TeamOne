import { Steps } from 'antd';
import React, { FC } from 'react';

import StepTimeline from './StepTimeline';
import { NodeType } from '../models/NodeType';
import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { NodeStatus } from '../models/NodeStatus';
import { TripBaseModel } from '../models/TripBaseModel';

const { Step } = Steps;

interface StepsToDoProps {
  items: ToDoNodeModel[];
  trip: TripBaseModel;
  onAddToDoNode: (toDoNode: ToDoNodeBaseModel) => void;
  onRemoveToDoNode: (id: string) => void;
  onToDoNodeStatusChange: (id: string, status: NodeStatus) => void;
  onToDoNodeUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const StepsToDo: FC<StepsToDoProps> = ({ items, trip, onAddToDoNode, onRemoveToDoNode, onToDoNodeStatusChange, onToDoNodeUpdate }: StepsToDoProps) => {
  const [current, setCurrent] = React.useState(NodeType.Before);

  const onChange = (current: any) => {
    console.log('onChange:', current);
    setCurrent(current);
  };

  const onAddClicked = () => {
    onAddToDoNode({ type: current });
  };

  return (
    <>
      <Steps
        type="navigation"
        current={current}
        onChange={onChange}
        className="site-navigation-steps"
      >
        <Step key="before" status="process" title="Before" />
        <Step key="during" status="process" title="During" />
        <Step key="after" status="process" title="After" />
      </Steps>
      <div className='steps-content'>
        <StepTimeline
          items={(items || []).filter(item => item.type === current)}
          trip={trip}
          type={current}
          onAddClicked={onAddClicked}
          onRemoveClicked={onRemoveToDoNode}
          onStatusChanged={onToDoNodeStatusChange}
          onUpdate={onToDoNodeUpdate} />
      </div>
    </>
  );
};

export default StepsToDo;
