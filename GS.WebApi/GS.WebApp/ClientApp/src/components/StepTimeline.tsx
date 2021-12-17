import React, { FC, useState } from 'react';
import moment from 'moment';
import { Timeline, Modal, Input, DatePicker, Button, Radio, Row, Col } from 'antd';

import { PlusCircleOutlined } from '@ant-design/icons';
import { ExclamationCircleOutlined } from '@ant-design/icons';

import { ToDoNodeModel } from '../models/ToDoNodeModel';
import { ToDoNodeBaseModel } from '../models/ToDoNodeBaseModel';
import { NodeStatus } from '../models/NodeStatus';
import { NodeType } from '../models/NodeType';
import { TimelineColorType } from '../models/TimelineColorType';
import { TripBaseModel } from '../models/TripBaseModel';
import { Tooltip, Progress } from 'antd';

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
  counter: number;
  onRemoveClicked: (id: string) => void;
  onStatusChanged: (id: string, status: NodeStatus) => void;
  onUpdate: (id: string, model: ToDoNodeBaseModel) => void;
}

const ToDoNodeEdit: FC<ToDoNodeEditProps> = ({ item, counter, onRemoveClicked, onStatusChanged, onUpdate }: ToDoNodeEditProps) => {

  const onNameChange = (e: any) => {
    item.name = e.target.value;
    onUpdate(item.id, item as ToDoNodeBaseModel);
  }

  const onDescriptionChange = (e: any) => {
    item.description = e.target.value;
    onUpdate(item.id, item as ToDoNodeBaseModel);
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
      <div className="edit-todo-text">
        <Input style={(counter % 2 === 0) ? {textAlign: "right"} : {textAlign: "left"}} defaultValue={item.name} bordered={false}
        placeholder='Name...' onBlur={onNameChange}/>
        <Input.TextArea style={(counter % 2 === 0) ? {textAlign: "right"} : {textAlign: "left"}} defaultValue={item.description} bordered={false} 
        placeholder='Description...' onBlur={onDescriptionChange}/>
      </div>
      <Radio.Group
        options={options}
        onChange={e => onStatusChanged(item.id, e.target.value)}
        value={item.status}
        optionType='button'
        buttonStyle='solid'
      />
      <Button type="primary" danger onClick={() => showConfirm(item.id, item.name)}>Remove</Button>
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
      <h3>{item.name ? item.name : DEFAULT_NAME}</h3>
      <p>{item.description}</p>
      <Radio.Group
        options={options}
        onChange={e => onStatusChanged(item.id, e.target.value)}
        value={item.status}
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
    if (moment(item.date) === date)
      return;
    item.date = date?.format(DATE_FORMAT);
    onUpdate(item.id, item as ToDoNodeBaseModel);
  };

  return (
    <DatePicker showTime className="edit-todo-dates" defaultValue={item.date ? moment(item.date) : undefined}
     bordered={false} placeholder={DATE_FORMAT} onChange={onChange} 
     disabledDate={d => !d || d.isAfter(trip.endDate) || d.isSameOrBefore(trip.startDate) }/>
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

  const sortItems = (items: ToDoNodeModel[]) => {
    const itemsWithDate = items.filter(item => item.date !== null).sort((a, b) => moment(a.date) > moment(b.date) ? 1 : -1);
    const itemsWithoutDate = items.filter(item => item.date === null);
    return itemsWithDate.concat(itemsWithoutDate);
  }

  const countItems = (items: ToDoNodeModel[], status: NodeStatus) => {
    return items.filter(item => item.status === status).length
  }
  
  const countDoneItems = countItems(items, NodeStatus.Done);
  const countInProgress = countItems(items, NodeStatus.InProgress);
  const countToDoItems = countItems(items, NodeStatus.ToDo);

  return (
    <>
      <Row justify="center" className='progressBar'>
        <Col xs={22} sm={20} md={18} lg={16}>
          <Tooltip title={countDoneItems + " done / " + countInProgress + " in progress / " + countToDoItems + " to do"}>
            <Progress percent={Math.round(((countDoneItems + countInProgress) / items.length * 100) * 1e2 / 1e2)}
             success={{ percent: countDoneItems / items.length * 100}} />
          </Tooltip>
        </Col>
      </Row>
      {isEditing ?
        <Row justify="center"> 
          <Button className="todo-button done" onClick={() => setEditing(false)}>Done</Button>
        </Row>
        :
        <Row justify="center">
          <Button className="todo-button edit" onClick={() => setEditing(true)}>Edit</Button>
        </Row>
      }
      <Timeline mode="alternate">
        {sortItems(items).map((item, index) => {
          return (
            isEditing ?
              <Timeline.Item key={item.id} color={TimelineColorType[type]} label={<ToDoNodeDateEdit item={item} trip={trip} onUpdate={onUpdate}/>}>
                <ToDoNodeEdit item={item} onRemoveClicked={onRemoveClicked} 
                onStatusChanged={onStatusChanged} onUpdate={onUpdate} counter={index+1} />
              </Timeline.Item>
              :
              <Timeline.Item key={item.id} color={TimelineColorType[type]} label={item.date ? moment(item.date).format(DATE_FORMAT) : undefined}>
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