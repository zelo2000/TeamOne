import React, { FC } from 'react';
import { Form, Input, Button, Row, Col } from 'antd';

interface TripDesctiprionFormProps {
  name?: string;
  description?: string;
}

const TripDescriptionForm: FC<TripDesctiprionFormProps> = ({name, description}: TripDesctiprionFormProps) => {
  const onFinish = (values: any) => {
    console.log(values);
  };

  return (
    <Form
      layout="vertical"
      onFinish={onFinish}
      initialValues={{Name: name, Description: description}}
      className="trip-form"
    >
      <Form.Item name="Name" label="Name">
        <Input/>
      </Form.Item>
      <Form.Item name="Description" label="Description">
        <Input.TextArea/>
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