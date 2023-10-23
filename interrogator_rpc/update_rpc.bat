



python -m grpc_tools.protoc -I./rpc_proto/ --python_out=rpc_proto/ --pyi_out=rpc_proto/ --grpc_python_out=rpc_proto/ rpc_proto/services.proto