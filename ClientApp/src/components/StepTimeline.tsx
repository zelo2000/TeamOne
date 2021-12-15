import React, { FC, useState } from 'react';
import { Timeline, Modal, Input, DatePicker, Button, Radio } from 'antd';
import moment from 'moment';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { PlusCircleOutlined } from '@ant-design/icons';
import { NodeStatus } from '../models/NodeStatus';
import { NodeType } from '../models/NodeType';
import { TimelineColorType } from '../models/TimelineColorType';
import { ExclamationCircleOutlined } from '@ant-design/icons';

const { confirm } = Modal;

const options = [
  { label: "To Do", value: NodeStatus.ToDo},
  { label: "In Progress", value: NodeStatus.InProgress},
  { label: "Done", value: NodeStatus.Done}
]

interface ToDoNodeEditProps {
  item: ToDoNodeModel;
  onRemoveClicked: (id: string) => void;
  onStatusChanged: (id: string, status: NodeStatus) => void;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const ToDoNodeEdit: FC<ToDoNodeEditProps> = ({ item, onRemoveClicked, onStatusChanged, onUpdate }: ToDoNodeEditProps) => {
  const onNameChange = (e: any) => {
    item.Name = e.target.value;
    onUpdate(item.Id, item as ToDoNodeBaseModel);
  }

  const onDescriptionChange = (e: any) => {
    item.Description = e.target.value;
    onUpdate(item.Id, item as ToDoNodeBaseModel);
  }

  const showConfirm = (id: string, name?: string) => {
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
    <div>
      <Input defaultValue={item.Name} bordered={false} placeholder='Name...' onBlur={onNameChange}/>
      <Input.TextArea defaultValue={item.Description} bordered={false} placeholder='Description...' onBlur={onDescriptionChange}/>
      <Radio.Group
        options={options}
        onChange={e => onStatusChanged(item.Id, e.target.value)}
        value={item.Status}
        optionType='button'
        buttonStyle='solid'
      />
      <Button type="primary" danger onClick={() => showConfirm(item.Id, item.Name)}>Remove</Button>
    </div>
  );
}

interface ToDoNodeViewProps {
  item: ToDoNodeModel;
  onStatusChanged: (id: string, status: NodeStatus) => void;
}

const ToDoNodeView: FC<ToDoNodeViewProps> = ({ item, onStatusChanged }: ToDoNodeViewProps) => {
  return (
    <div>
      <h1>{item.Name}</h1>
      <p>{item.Description}</p>
      <Radio.Group
        options={options}
        onChange={e => onStatusChanged(item.Id, e.target.value)}
        value={item.Status}
        optionType='button'
        buttonStyle='solid'
      />
    </div>
  );
}

interface ToDoNodeDateEditProps {
  item: ToDoNodeModel;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const ToDoNodeDateEdit: FC<ToDoNodeDateEditProps> = ({ item, onUpdate }: ToDoNodeDateEditProps) => {
  const onChange = (date: moment.Moment | null, dateString: string) => {
    if (moment(item.Date) === date)
      return;
    item.Date = date?.format('YYYY-MM-DD');
    onUpdate(item.Id, item as ToDoNodeBaseModel);
  };

  return (
    <DatePicker defaultValue={item.Date ? moment(item.Date) : undefined} bordered={false} placeholder='YYYY-MM-DD' onChange={onChange}/>
  );
}

interface StepTimelineProps {
  items: ToDoNodeModel[];
  type: NodeType;
  onAddClicked: () => void;
  onRemoveClicked: (id: string) => void;
  onStatusChanged: (id: string, status: NodeStatus) => void;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, type, onAddClicked, onRemoveClicked, onStatusChanged, onUpdate }: StepTimelineProps) => {
  const [isEditing, setEditing] = useState(false);

  return (
    <>
      {isEditing ?
        <Button onClick={() => setEditing(false)}>Done</Button>
        :
        <Button onClick={() => setEditing(true)}>Edit</Button>
      }
      <Timeline mode="alternate">
        {items.map(item => {
          return (
            isEditing ?
              <Timeline.Item color={TimelineColorType[type]} label={<ToDoNodeDateEdit item={item} onUpdate={onUpdate}/>}>
                <ToDoNodeEdit item={item} onRemoveClicked={onRemoveClicked} onStatusChanged={onStatusChanged} onUpdate={onUpdate}/>
              </Timeline.Item>
              :
              <Timeline.Item color={TimelineColorType[type]} label={item.Date ? moment(item.Date).format("YYYY-MM-DD") : undefined}>
                <ToDoNodeView item={item} onStatusChanged={onStatusChanged}/>
              </Timeline.Item>
          );
        })}
        <Timeline.Item
          color="green"
          dot={<PlusCircleOutlined style={{ fontSize: '30px' }}
            onClick={() => {
              onAddClicked();
              setEditing(true);
            }}/>
          }
        />
      </Timeline>
    </>

  );
};

export default StepTimeline;