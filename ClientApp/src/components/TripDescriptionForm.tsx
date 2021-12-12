import React, { FC } from 'react';
import { Form, Input, DatePicker, Button, Row, Col } from 'antd';
import moment from 'moment';

import { TripBaseModel } from "../models/TripBaseModel";

const { RangePicker } = DatePicker;

interface TripDesctiprionFormProps {
  id: string;
  name?: string;
  description?: string;
}

const TripDescriptionForm: FC<TripBaseModel> = ({Name, Description, StartDate, EndDate }: TripBaseModel) => {
  const onFinish = (values: any) => {
    console.log(values);
  };

  const dateFormat = 'YYYY-MM-DD';

  return (
    <Form
      layout="vertical"
      onFinish={onFinish}
      initialValues={{name: Name, description: Description, rangePicker: [moment(StartDate), moment(EndDate)]}}
      className="trip-form"
    >
      <Form.Item name="name" label="Name">
        <Input/>
      </Form.Item>
      <Form.Item name="description" label="Description">
        <Input.TextArea/>
      </Form.Item>
      <Form.Item name="rangePicker" label="Dates">
        <RangePicker format={dateFormat}/>
      </Form.Item>
      <Form.Item>
        <Row>
          <Col 
            xs={{ span: 12, offset: 6 }} 
            sm={{ span: 10, offset: 14 }} 
            md={{ span: 8, offset: 16 }}
            lg={{ span: 7, offset: 17 }}
            xl={{ span: 5, offset: 19 }}
            className="trip-button">
            <Button type="primary" htmlType="submit">
              Save
            </Button>
          </Col>
        </Row>
      </Form.Item>
    </Form>
  );
};

export default TripDescriptionForm;