class StatisticHandler {
  constructor() {
    this.colors = {
      5: "#1167bf",
      4: "#2390ff",
      3: "#58a8f9",
      2: "#85c0fb",
      1: "#abd1f7",
      0: "#e9ecef"
    };
    this.length = 6;
    this.groups = null;
    this.count = null;
    this.percents = null;
    document.addEventListener("DOMContentLoaded", () => this.createChart());
  }

  async createChart() {
    let root = document.getElementById("statistic");
    if (!root)
      return;
    root.classList.add("progress");
    await this.loadData();
    if (!(this.groups && this.count && this.percents))
      return new Error("No data");
    new Array(this.length)
      .fill(null)
      .forEach((elem, i) => {
        let index = this.length - 1 - i;
        elem = document.createElement("div");
        elem.classList.add("progress-bar");
        elem.setAttribute("id", this.getId(index));
        elem.setAttribute("role", "progressbar");
        elem.setAttribute("aria-valuemin", "0");
        elem.setAttribute("aria-valuemax", "100");
        elem.setAttribute("aria-valuenow", this.percents[index]);
        elem.setAttribute("title", `${this.percents[index]}%`);
        elem.style.width = `${this.percents[index]}%`;
        elem.style.backgroundColor = this.colors[index];
        root.appendChild(elem);
      });
  }

  async loadData() {
    const response = await fetch("/Words/Statistic");
    if (response.ok) {
      const data = await response.json();
      if (data.groups.length !== this.length)
        return new Error(`Statistic data must have ${this.length} elements`);
      this.groups = { ...data.groups };
      this.count = data.count;
      this.percents = { ...data.groups.map(x => this.getPercents(x)) };
    }
  }

  getId(index) {
    return `group${index}`;
  }

  getPercents(count) {
    return (count / this.count * 100).toFixed(2);
  }

  update(group, success) {
    if (!(this.groups && this.count && this.percents))
      return new Error("No data");
    if (group < 0 || group > this.length - 1)
      return new Error("Argument exception");
    if ((group == 0 && !success) || (group == 5 && success))
      return;

    let upGroup = ((success, group) => {
      if (!success && group === 5)
        return 3;
      return success ? group + 1 : group - 1;
    })(success, group);

    this.groups[group]--;
    this.groups[upGroup]++;
    this.percents[group] = this.getPercents(this.groups[group]);
    this.percents[upGroup] = this.getPercents(this.groups[upGroup]);
    [upGroup, group].forEach(index => {
      let elem = document.getElementById(this.getId(index));
      elem.setAttribute("aria-valuenow", this.percents[index]);
      elem.setAttribute("title", `${this.percents[index]}%`);
      elem.style.width = `${this.percents[index]}%`;
    });
  }
}

export default StatisticHandler;
