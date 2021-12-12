import React, { FC } from 'react';
import { Card, Col, Row } from 'antd';

const { Meta } = Card;

const CardList: FC = () => {
  return (
    <div className="site-card-wrapper">
    <Row gutter={16}>
      <Col span={8}>
        <Card 
          bordered={false} 
          hoverable={true}
          cover={<div className="trip-img"></div>}
        >
          <Meta
            title="Trip title"
            description="Trip content"
          />
        </Card>
      </Col>
    </Row>
  </div>
  );
};

export default CardList;