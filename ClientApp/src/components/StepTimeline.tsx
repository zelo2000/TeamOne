import React, { FC } from 'react';
import { Timeline } from 'antd';

interface StepTimelineProps {
  items: string[];
  color: string;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, color }: StepTimelineProps) => {
  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item color={color}>{item}</Timeline.Item>
        );
      })}
    </Timeline>
  );
};

export default StepTimeline;