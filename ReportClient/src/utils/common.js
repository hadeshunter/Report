export function removeSpace(text) {
  return text.split(" ").join("_");
}

export function getDiff(arr1, arr2) {
  let unique1 = arr1.filter(o => arr2.indexOf(o) === -1);
  let unique2 = arr2.filter(o => arr1.indexOf(o) === -1);

  const unique = unique1.concat(unique2);
  return unique;
}

export function getSLAReview(id) {
  if (id == 1) {
    return "Đúng hạn";
  } else if (id == 2) {
    return "Trễ hạn";
  } else if (id == 3) {
    return "Chưa bắt đầu";
  } else if (id == 4) {
    return "Chưa kết thúc";
  } else {
    return id;
  }
}
