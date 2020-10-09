
```
$ ansible-playbook src/modules/windows_worker/build_infrastructure.yml -vv 
--extra-vars 
    '{
      "resource_group_name":"tim-rg-le", 
      "vnet_name":"vnet-le", 
      "subnet_name":"tim-rg-le-subnet-0", 
      "location":"westus", 
      "username":"braveheart", 
      "password":"P@ssword1234!", 
      "machine_name":"devWinTim",
      "machine_size":"Standard_DS4_v2", 
      "module_id":"" 
    }'
```

or single line
```
ansible-playbook src/modules/windows_worker/build_infrastructure.yml -vv --extra-vars '{"resource_group_name":"tim-rg-le", "vnet_name":"vnet-le", "subnet_name":"tim-rg-le-subnet-0", "location":"westus", "username":"braveheart", "password":"P@ssword1234!", "machine_name":"devWinTim", 
"machine_size":"Standard_DS4_v2", "module_id":"" }'
```

From Devops API (after pipeline creation):
```
{
  "resources": {
    "repositories": {
      "self": {
        "refName": "refs/heads/feature/basic-ubuntu-module"
      }
    }
  },
  "templateParameters": { 
    "REGION": "westus",
    "RESOURCE_GROUP_NAME": "tim-rg-le",
    "VNET_NAME": "vnet-le",
    "SUBNET_NAME": "tim-rg-le-subnet-0",
    "MACHINE_USERNAME": "braveheart",
    "MACHINE_PASSWORD": "P@ssword1234!",
    "MACHINE_NAME": "AutoUbuTwo",
    "MACHINE_SIZE": "Standard_DS4_v2",
    "TAG_ENVIRONMENT": "dev"
    "MODULE_ID": "421" # REMOVE MODULE_ID IF DEPLOYING NET NEW MODULE AND AN ID WILL BE ASSIGNED, OR PASS A THREE DIGIT INTEGER AS A STRING TO SPECIFY AN ID OR TARGET AN EXISTING DEPLOYMENT
  },
  "variables": {}
}
``` 

Approximate Endpoint minus BUILD_NUMBER_ID 

https://{{coreServer}}/{{organization}}/{{project}}/_apis/pipelines/BUILD_NUMBER_ID/runs?api-version={{api-version-preview}}