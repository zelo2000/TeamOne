import { Steps } from 'antd';
import React, { FC } from 'react';

const { Step } = Steps;

const StepsToDo: FC = () => {
  const [ current, setCurrent ] = React.useState(0);

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
        <Step status="finish" title="Step 1" />
        <Step status="process" title="Step 2" />
        <Step status="wait" title="Step 3" />
      </Steps>
    </>
  );
};

export default StepsToDo;
