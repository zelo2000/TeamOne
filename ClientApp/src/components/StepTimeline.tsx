import React, { FC } from 'react';
import { Timeline, DatePicker, Button, Radio } from 'antd';
import moment from 'moment';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { PlusCircleOutlined } from '@ant-design/icons';
import { NodeStatus } from '../models/NodeStatus';

const options = [
  { label: "To Do", value: NodeStatus.ToDo},
  { label: "In Progress", value: NodeStatus.InProgress},
  { label: "Done", value: NodeStatus.Done}
]

interface StepTimelineProps {
  items: ToDoNodeModel[];
  color: string;
  onAddClicked: () => void;
  onRemoveClicked: (id: string) => void;
  onStatusChanged: (id: string, status: NodeStatus) => void;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, color, onAddClicked, onRemoveClicked, onStatusChanged }: StepTimelineProps) => {
  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item key={item.Id} color={color} label={item.Date ? moment(item.Date).format("YYYY-MM-DD") : undefined}>
            <h1>{item.Name}</h1>
            <p>{item.Description}</p>
            <Radio.Group
              options={options}
              onChange={e => onStatusChanged(item.Id, e.target.value)}
              value={item.Status}
              optionType='button'
              buttonStyle='solid'
            />
            <Button onClick={() => onRemoveClicked(item.Id)}>Remove</Button>
          </Timeline.Item>
        );
      })}
      <Timeline.Item color="green" dot={<PlusCircleOutlined style={{ fontSize: '36px' }} onClick={onAddClicked}/>}/>
    </Timeline>
  );
};

export default StepTimeline;