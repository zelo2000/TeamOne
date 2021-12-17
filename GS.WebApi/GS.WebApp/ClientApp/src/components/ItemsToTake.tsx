import { Button, Checkbox, Col, Divider, Input, Modal, Progress, Row, Tooltip, Typography } from 'antd';
import { FC, useState } from 'react';

import { ItemToTakeModel } from '../models/ItemToTakeModel';
import { ItemToTakeBaseModel } from '../models/ItemToTakeBaseModel';
import { ExclamationCircleOutlined } from '@ant-design/icons';

const { Title } = Typography;
const { confirm } = Modal;
const DEFAULT_NAME = 'Unnamed item';

interface ItemstoTakeProps {
  items: ItemToTakeModel[];
  onAddItemToTake: () => void;
  onRemoveItemToTake: (id: string) => void;
  onItemToTakeStatusChange: (id: string, status: boolean) => void;
  onItemToTakeUpdate: (id: string, model: ItemToTakeBaseModel) => void;
}

const ItemstoTake: FC<ItemstoTakeProps> = ({ items, onAddItemToTake, onRemoveItemToTake, onItemToTakeStatusChange, onItemToTakeUpdate}: ItemstoTakeProps) => {
  const defaultCheckedList = items.filter(item => item.isTaken === true).map(item => (item.name || ""));
  
  const onChange = (checkedValues: any) => {
    return (
      items.map(item => (checkedValues.includes(item.name) ?
       onItemToTakeStatusChange(item.id, true) : onItemToTakeStatusChange(item.id, false))));
  }

  
  const onNameChange = (item: ItemToTakeModel, e: any) => {
    item.name = e.target.value;
    onItemToTakeUpdate(item.id, item);
  }

  const showConfirm = (id: string, name?: string) => {
    confirm({
      title: 'Do you want to delete this item?',
      closable: true,
      maskClosable: true,
      icon: <ExclamationCircleOutlined />,
      content: name ? name: DEFAULT_NAME,
      onOk() {
        onRemoveItemToTake(id);
      },
    });
  }

  const countItems = (items: ItemToTakeModel[], status: boolean) => {
    return items.filter(item => item.isTaken === status).length
  }
  
  const countTakenItems = countItems(items, true);
  const countToTakeItems = countItems(items, false);

  return (
    <>
    <Row justify="center" className='progressBar'>
      <Col xs={24} sm={23} md={22} lg={21}>
        <Tooltip title={countTakenItems + " taken / " + countToTakeItems + " to take"}>
          <Progress percent={Math.round((countTakenItems / items.length * 100) * 1e2 ) / 1e2} 
          success={{ percent: countTakenItems / items.length * 100}} />
        </Tooltip>
      </Col>
    </Row>
    <Row>
    <Title level={4}>Items to take</Title>
      <Divider />
      <Row gutter={[0, 16]} className='items-to-take-container'>
        <Col span={24}>
          <Checkbox.Group onChange={onChange} value={defaultCheckedList}>
            <Row gutter={[12, 12]}>
            {(items).map((item) => {
              return (
              <Col span={8}>
                <Checkbox value={item.name}>
                  <Row justify='center'>
                    <Col span={12} style={{ textAlign: 'center'}}>
                      <Input defaultValue={item.name} bordered={false} placeholder='Name...' onBlur={e => onNameChange(item, e)}/>
                    </Col>
                    <Col span={12}>
                      <Button type="primary" danger onClick={() => showConfirm(item.id, item.name)}>Remove</Button>
                    </Col>
                  </Row>
                </Checkbox>
              </Col>
              );
            })}
            </Row>
          </Checkbox.Group> 
        </Col>
`        <Col
          xs={{ span: 12, offset: 6 }} 
          sm={{ span: 9, offset: 15 }} 
          md={{ span: 7, offset: 17 }}
          lg={{ span: 6, offset: 18 }}
          xl={{ span: 4, offset: 20 }}
        >
          <Button type="primary" onClick={() => onAddItemToTake()}>Add</Button>
        </Col>`
      </Row>
    </Row>
    </>
  );
};

export default ItemstoTake;