import React, { FC } from 'react';
import { Form, Input, DatePicker, Button, Row, Col } from 'antd';
import moment from 'moment';

import { TripBaseModel } from "../models/TripBaseModel";

const { RangePicker } = DatePicker;

interface TripDesctiprionFormProps {
  trip: TripBaseModel;
  onSubmit: (trip: TripBaseModel) => void;
}

const TripDescriptionForm: FC<TripDesctiprionFormProps> = ({ trip, onSubmit }: TripDesctiprionFormProps) => {
  const dateFormat = 'YYYY-MM-DD';

  const onFinish = (values: any) => {
    const tripModel: TripBaseModel = {
      userId: trip.userId,
      name: values.name,
      description: values.description,
      startDate: values.dates[0].format(dateFormat),
      endDate: values.dates[1].format(dateFormat)
    };
    onSubmit(tripModel);
  };

  return (
    <Form
      layout="vertical"
      onFinish={onFinish}
      initialValues={{
        name: trip.name,
        description: trip.description,
        dates: (trip.startDate && trip.endDate) ? [moment(trip.startDate), moment(trip.endDate)] : []
      }}
      className="trip-form"
    >
      <Form.Item name="name" label="Name">
        <Input/>
      </Form.Item>
      <Form.Item name="description" label="Description">
        <Input.TextArea/>
      </Form.Item>
      <Form.Item name="dates" label="Dates">
        <RangePicker disabledDate={d => !d || d.isSameOrBefore(new Date().toISOString().slice(0, 10)) }
         format={dateFormat}/>
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