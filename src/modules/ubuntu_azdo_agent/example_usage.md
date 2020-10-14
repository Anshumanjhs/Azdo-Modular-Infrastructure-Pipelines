```
export ARM_ACCESS_KEY=
export STORAGE_ACCOUNT_NAME=
export STORAGE_STATE_CONTAINER_NAME=
```


```
$ ansible-playbook src/modules/ubuntu_azdo_agent/build_infrastructure.yml -vv --extra-vars '{"location":"westus2",  "admin_username":"braveheart", "admin_password":"P@ssword1234!" }'
```

