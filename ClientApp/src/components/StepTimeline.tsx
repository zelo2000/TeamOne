import React, { FC } from 'react';
import { Timeline, DatePicker } from 'antd';
import moment from 'moment';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';

interface StepTimelineProps {
  items: ToDoNodeModel[];
  color: string;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, color }: StepTimelineProps) => {
  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item color={color} label={item.Date ? moment(item.Date).format("YYYY-MM-DD") : undefined}>
            <h1>{item.Name}</h1>
            <p>{item.Description}</p>
          </Timeline.Item>
        );
      })}
    </Timeline>
  );
};

export default StepTimeline;