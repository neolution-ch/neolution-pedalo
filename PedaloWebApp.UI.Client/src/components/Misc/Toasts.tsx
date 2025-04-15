import React from "react";
import { Button, Media } from "reactstrap";
import { toast } from "react-toastify";
import getNextConfig from "next/config";

interface ContentErrorProps {
  message: string;
  closeToast?: () => void;
}

const ContentError = ({ message, closeToast }: ContentErrorProps) => {
  const {
    publicRuntimeConfig: { showCopyErrorMessageButton },
  } = getNextConfig();

  return (
    <Media>
      <Media middle left className="mr-3">
        <i className="fa fa-fw fa-2x fa-close" />
      </Media>
      <Media body>
        <Media heading tag="h6">
          Error
        </Media>
        {message.split("\\r\\n").map((m, i) => (
          <p key={i} className="mb-0">
            {m}
          </p>
        ))}

        <div className="d-flex mt-2">
          <Button
            color="danger"
            onClick={() => {
              if (closeToast) closeToast();
            }}
          >
            Schliessen
          </Button>

          {showCopyErrorMessageButton && (
            <Button
              color="warning"
              onClick={() => {
                navigator.clipboard.writeText(message);
              }}
            >
              Copy To clipboard
            </Button>
          )}
        </div>
      </Media>
    </Media>
  );
};

interface ContentSuccessProps {
  message: string;
  closeToast?: () => void;
}

const ContentSuccess = ({ message, closeToast }: ContentSuccessProps) => (
  <Media>
    <Media middle left className="mr-3">
      <i className="fa fa-fw fa-2x fa-check" />
    </Media>
    <Media body>
      <Media heading tag="h6">
        Erfolg
      </Media>
      <p>{message}</p>
      <div className="d-flex mt-2">
        <Button
          color="success"
          onClick={() => {
            if (closeToast) closeToast();
          }}
        >
          Schliessen
        </Button>
      </div>
    </Media>
  </Media>
);

const toastError = (message?: string) => {
  toast.error(<ContentError message={message || "unexpected error"} />, { autoClose: 2000, position: "top-center" });
};

const toastSuccess = (message: string) => {
  toast.success(<ContentSuccess message={message} />, { autoClose: 2000, position: "top-center" });
};

export { toastError, toastSuccess };
