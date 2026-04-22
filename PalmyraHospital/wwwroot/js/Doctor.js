document.addEventListener("DOMContentLoaded", function () {

    const departmentDropdown = document.getElementById("departmentDropdown");
    const specializationDropdown = document.getElementById("specializationDropdown");

    if (!departmentDropdown) return; // حماية لو الصفحة مختلفة

    departmentDropdown.addEventListener("change", function () {

        const departmentId = this.value;

        specializationDropdown.innerHTML = '<option value="">Loading...</option>';
        specializationDropdown.disabled = true;

        if (!departmentId) {
            specializationDropdown.innerHTML = '<option value="">Select Specialization</option>';
            return;
        }

        fetch(`/Admin/Doctor/GetSpecializations?departmentId=${departmentId}`)
            .then(res => res.json())
            .then(data => {

                specializationDropdown.innerHTML = '<option value="">Select Specialization</option>';

                data.forEach(item => {
                    const option = document.createElement("option");
                    option.value = item.id;
                    option.text = item.name;
                    specializationDropdown.appendChild(option);
                });

                specializationDropdown.disabled = false;
            });
    });

});

document.addEventListener("DOMContentLoaded", function () {

    const departmentDropdown = document.getElementById("departmentDropdown");
    const specializationDropdown = document.getElementById("specializationDropdown");

    if (!departmentDropdown) return;

    departmentDropdown.addEventListener("change", function () {

        const departmentId = this.value;

        specializationDropdown.innerHTML = '<option value="">Loading...</option>';
        specializationDropdown.disabled = true;

        if (!departmentId) {
            specializationDropdown.innerHTML = '<option value="">Select Specialization</option>';
            return;
        }

        fetch(`/Admin/Doctor/GetSpecializations?departmentId=${departmentId}`)
            .then(res => res.json())
            .then(data => {

                specializationDropdown.innerHTML = '<option value="">Select Specialization</option>';

                data.forEach(item => {
                    const option = document.createElement("option");
                    option.value = item.id;
                    option.text = item.name;
                    specializationDropdown.appendChild(option);
                });

                specializationDropdown.disabled = false;
            })
            .catch(() => {
                specializationDropdown.innerHTML = '<option value="">Error loading data</option>';
            });
    });

});