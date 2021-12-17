import { FC } from "react";
import { Col, Row } from "antd";
import bg from "../assets/background.png";

const Home: FC = () => {
  return (
    <Row justify="center">
      <Col span={24} className="welcome-label">{"Welcome to Go & See. Login to see more... "}</Col>
      <Col span={24} className="bg-img"><img src={bg} alt="GoSee background"/></Col>
    </Row>
  );
}

export default Home;