import { AxiosInstance } from "axios";

function isIsoDateString(value: any): boolean {
  // matches the following patterns:
  // 2023-07-17T20:21:47.0076818+00:00
  // 2023-07-17
  // 2023-07-17T20:21:47.0076818Z
  const dateRegex = /^(\d{4}-\d{2}-\d{2})(T\d{2}:\d{2}:\d{2}(\.\d+)?(Z|(\+|-)\d{2}:\d{2})?)?$/;
  return value && typeof value === "string" && dateRegex.test(value);
}

function handleDates(body: any) {
  if (body === null || body === undefined || typeof body !== "object") return body;

  for (const key of Object.keys(body)) {
    const value = body[key];
    if (isIsoDateString(value)) {
      body[key] = new Date(value);
    } else if (typeof value === "object") {
      handleDates(value);
    }
  }
}

const registerDateResponseInterceptor = (instance: AxiosInstance): void => {
  instance.interceptors.response.use((response) => {
    handleDates(response.data);
    return response;
  });
};

export { registerDateResponseInterceptor };
