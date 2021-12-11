import React, { FC } from 'react';
import { Form, Input, Button } from 'antd';

interface TripDesctiprionFormProps {
  name: string;
  description: string;
}

const TripDescriptionForm: FC<TripDesctiprionFormProps> = ({name, description}: TripDesctiprionFormProps) => {
  const onFinish = (values: any) => {
    console.log(values);
  };

  return (
    <Form
      layout="vertical"
      onFinish={onFinish}
      initialValues={{name: name, description: description}}
    >
      <Form.Item name="name" label="Name">
        <Input/>
      </Form.Item>
      <Form.Item name="description" label="Description">
        <Input.TextArea/>
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Save
        </Button>
      </Form.Item>
    </Form>
  );
};

export default TripDescriptionForm;