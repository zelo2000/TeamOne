import { Steps } from 'antd';
import React, { FC } from 'react';

import StepTimeline from './StepTimeline';
import { TimelineColorType } from '../models/TimelineColorType';
import { NodeType } from '../models/NodeType';
import { ToDoNodeModel } from '../models/ToDoNodeModel';

const { Step } = Steps;

interface StepsToDoProps {
  items: ToDoNodeModel[];
}

const StepsToDo: FC<StepsToDoProps> = ({ items }: StepsToDoProps) => {
  const [ current, setCurrent ] = React.useState(NodeType.Before);

  const onChange = (current: any) => {
    console.log('onChange:', current);
    setCurrent(current);
  };

  return (
    <>
      <Steps
        type="navigation"
        current={current}
        onChange={onChange}
        className="site-navigation-steps"
      >
        <Step key="before" status="process" title="Before"/>
        <Step key="during" status="process" title="During"/>
        <Step key="after" status="process" title="After"/>
      </Steps>
      <div className='steps-content'>
        <StepTimeline items={items.filter(item => item.Type === current)} color={TimelineColorType[current]}/>
      </div>
    </>
  );
};

export default StepsToDo;
