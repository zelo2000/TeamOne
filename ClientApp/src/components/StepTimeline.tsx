import React, { FC } from 'react';
import { Timeline, DatePicker, Button } from 'antd';
import moment from 'moment';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { PlusCircleOutlined } from '@ant-design/icons';

interface StepTimelineProps {
  items: ToDoNodeModel[];
  color: string;
  onAddClicked: () => void;
  onRemoveClicked: (id: string) => void;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, color, onAddClicked, onRemoveClicked }: StepTimelineProps) => {
  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item key={item.Id} color={color} label={item.Date ? moment(item.Date).format("YYYY-MM-DD") : undefined}>
            <h1>{item.Name}</h1>
            <p>{item.Description}</p>
            <Button onClick={() => onRemoveClicked(item.Id)}>Remove</Button>
          </Timeline.Item>
        );
      })}
      <Timeline.Item color="green" dot={<PlusCircleOutlined style={{ fontSize: '36px' }} onClick={onAddClicked}/>}/>
    </Timeline>
  );
};

export default StepTimeline;