import { NextPage } from "next";
import { useSession } from "next-auth/react";
import { JWT, decode } from "next-auth/jwt";
import { useState } from "react";
import { Card, CardBody, Col, Row } from "reactstrap";

interface JwtState {
  token: string;
  secret: string;
}

const DebugPage: NextPage = () => {
  const { data: session, update } = useSession();
  const [jwtState, setJwtState] = useState<JwtState>({ token: "", secret: "" });
  const [jwtDecoded, setJwtDecoded] = useState<JWT | null | string>(null);
  return (
    <div>
      <h1>Debug Page</h1>
      <br />
      <Card className="ms-3 mb-5">
        <CardBody>
          <Row>
            <Col xl={12}>
              <h2>Session</h2>
            </Col>
            <Col xl={12}>
              <pre className="session">{JSON.stringify(session, null, 2)}</pre>
            </Col>

            <Col xl={12}>
              <button
                className="btn btn-secondary"
                onClick={async () =>
                  await update({
                    command: "refreshApiToken",
                  })
                }
              >
                Refresh Token
              </button>
            </Col>
          </Row>
        </CardBody>
      </Card>
      <Card className="ms-3 mb-5">
        <CardBody>
          <Row>
            <Col xl={6}>
              <Row>
                <Col xl={12}>
                  <h2>Next Auth</h2>
                </Col>
                <Col xl={12} className="mb-3">
                  <label className="form-label">Token</label>
                  <textarea
                    className="form-control"
                    onChange={(e) => setJwtState((prev: JwtState) => ({ ...prev, token: e.target.value || "" }))}
                  ></textarea>
                </Col>
                <Col xl={12} className="mb-3">
                  <label className="form-label">Secret</label>
                  <input
                    className="form-control"
                    type="text"
                    onChange={(e) => setJwtState((prev: JwtState) => ({ ...prev, secret: e.target.value || "" }))}
                  />
                </Col>
                <Col xl={12} className="mb-3">
                  <button
                    onClick={() => {
                      decode(jwtState)
                        .then((jwt) => setJwtDecoded(jwt))
                        .catch((e) => setJwtDecoded(`${e}`));
                    }}
                    className="btn btn-secondary"
                  >
                    Decode
                  </button>
                </Col>
              </Row>
            </Col>
            <Col xl={12} className="mb-3">
              <label className="form-label">Decoded:</label>
              <pre>{JSON.stringify(jwtDecoded)}</pre>
            </Col>
          </Row>
        </CardBody>
      </Card>
    </div>
  );
};

export default DebugPage;
