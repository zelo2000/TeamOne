import React, { FC, useState } from 'react';
import moment from 'moment';
import { Timeline, Modal, Input, DatePicker, Button, Radio, Row } from 'antd';

import { PlusCircleOutlined } from '@ant-design/icons';
import { ExclamationCircleOutlined } from '@ant-design/icons';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { NodeStatus } from '../models/NodeStatus';
import { NodeType } from '../models/NodeType';
import { TimelineColorType } from '../models/TimelineColorType';
import { TripBaseModel } from '../models/TripBaseModel';

const { confirm } = Modal;

const options = [
  { label: "To Do", value: NodeStatus.ToDo},
  { label: "In Progress", value: NodeStatus.InProgress},
  { label: "Done", value: NodeStatus.Done}
]

const DATE_FORMAT = 'YYYY-MM-DD HH:mm';
const DEFAULT_NAME = 'Unnamed todo'

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
      content: name ? name: DEFAULT_NAME,
      onOk() {
        onRemoveClicked(id);
      },
    });
  }
  
  return (
    <div>
      {<div className="edit-todo-text">
        <Input defaultValue={item.Name} bordered={false}
        placeholder='Name...' onBlur={onNameChange}/>
        <Input.TextArea defaultValue={item.Description} bordered={false} 
        placeholder='Description...' onBlur={onDescriptionChange}/>
      </div>
      } 
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
      <h3>{item.Name ? item.Name : DEFAULT_NAME}</h3>
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
  trip: TripBaseModel;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const ToDoNodeDateEdit: FC<ToDoNodeDateEditProps> = ({ item, trip, onUpdate }: ToDoNodeDateEditProps) => {
  const onChange = (date: moment.Moment | null, dateString: string) => {
    if (moment(item.Date) === date)
      return;
    item.Date = date?.format(DATE_FORMAT);
    onUpdate(item.Id, item as ToDoNodeBaseModel);
  };

  return (
    <DatePicker showTime className="edit-todo-dates" defaultValue={item.Date ? moment(item.Date) : undefined}
     bordered={false} placeholder={DATE_FORMAT} onChange={onChange} 
     disabledDate={d => !d || d.isAfter(trip.EndDate) || d.isSameOrBefore(trip.StartDate) }/>
  );
}

interface StepTimelineProps {
  items: ToDoNodeModel[];
  trip: TripBaseModel;
  type: NodeType;
  onAddClicked: () => void;
  onRemoveClicked: (id: string) => void;
  onStatusChanged: (id: string, status: NodeStatus) => void;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const StepTimeline: FC<StepTimelineProps> = ({ items, trip, type, onAddClicked, onRemoveClicked, onStatusChanged, onUpdate }: StepTimelineProps) => {
  const [isEditing, setEditing] = useState(false);

  return (
    <>
      {isEditing ?
        <Row justify="center"> 
          <Button className="todo-button done" onClick={() => setEditing(false)}>Done</Button>
        </Row>
        :
        <Row justify="center">
          <Button className="todo-button edit" onClick={() => setEditing(true)}>Edit</Button>
        </Row>
      }
      <Timeline mode="left">
        {items.map(item => {
          return (
            isEditing ?
              <Timeline.Item color={TimelineColorType[type]} label={<ToDoNodeDateEdit item={item} trip={trip} onUpdate={onUpdate}/>}>
                <ToDoNodeEdit item={item} onRemoveClicked={onRemoveClicked} 
                onStatusChanged={onStatusChanged} onUpdate={onUpdate} />
              </Timeline.Item>
              :
              <Timeline.Item color={TimelineColorType[type]} label={item.Date ? moment(item.Date).format(DATE_FORMAT) : undefined}>
                <ToDoNodeView item={item} onStatusChanged={onStatusChanged}/>
              </Timeline.Item>
          );
        })}
        <Timeline.Item
          className="add-todo-button"
          dot={<PlusCircleOutlined
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