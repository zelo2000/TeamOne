import React, { FC } from 'react';
import { Timeline } from 'antd';

interface StepTimelineProps {
  items: string[];
}

const StepTimeline: FC<StepTimelineProps> = ({ items }: StepTimelineProps) => {
  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item>{item}</Timeline.Item>
        );
      })}
    </Timeline>
  );
};

export default StepTimeline;