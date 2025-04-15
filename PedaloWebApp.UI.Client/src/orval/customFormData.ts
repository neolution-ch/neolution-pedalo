const customFormData = <Body>(body: Body): FormData => {
  const formData = new FormData();

  // Assuming the 'body' parameter is an object with key-value pairs,
  // we can loop through its properties and append them to the FormData.
  for (const key in body) {
    // 'hasOwnProperty' check is used to ensure we only get the object's own properties.
    if (Object.prototype.hasOwnProperty.call(body, key)) {
      const value = body[key];
      formData.append(key, value as string);
    }
  }

  return formData;
};

export { customFormData };
