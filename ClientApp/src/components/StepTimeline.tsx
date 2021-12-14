import React, { FC } from 'react';
import { Timeline, Modal, Button, Radio} from 'antd';
import moment from 'moment';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { PlusCircleOutlined } from '@ant-design/icons';
import { NodeStatus } from '../models/NodeStatus';
import { ExclamationCircleOutlined } from '@ant-design/icons';

const { confirm } = Modal;

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
  const showConfirm = (name : any, id: string) => {
    confirm({
      title: 'Do you want to delete this todo?',
      closable: true,
      maskClosable: true,
      icon: <ExclamationCircleOutlined />,
      content: name ? name: "Next todo",
      onOk() {
        onRemoveClicked(id);
      },
    });
  }

  return (
    <Timeline mode="alternate">
      {items.map(item => {
        return (
          <Timeline.Item key={item.Id} color={color} label={item.Date ? moment(item.Date).format("YYYY-MM-DD") : undefined}>
            <h3>{item.Name ? item.Name: "Next todo"}</h3>
            <p>{item.Description}</p>
            <Radio.Group
              options={options}
              onChange={e => onStatusChanged(item.Id, e.target.value)}
              value={item.Status}
              optionType='button'
              buttonStyle='solid'
            />
            <Button type="primary" danger onClick={() => showConfirm(item.Name, item.Id)}>Remove</Button>
          </Timeline.Item>
        );
      })}
      <Timeline.Item color="green" dot={<PlusCircleOutlined 
        style={{ fontSize: '30px' }} onClick={onAddClicked}/>}/>
    </Timeline>
  );
};

export default StepTimeline;